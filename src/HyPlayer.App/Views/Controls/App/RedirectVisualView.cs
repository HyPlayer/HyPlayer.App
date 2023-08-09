﻿using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CoolControls.WinUI3.Controls
{
    [ContentProperty(Name = nameof(Child))]
    public class RedirectVisualView : Control
    {
        public RedirectVisualView()
        {
            this.DefaultStyleKey = typeof(RedirectVisualView);

            childVisualBrushOffsetEnabled = ChildVisualBrushOffsetEnabled;

            hostVisual = ElementCompositionPreview.GetElementVisual(this);
            compositor = hostVisual.Compositor;

            childVisualSurface = compositor.CreateVisualSurface();
            childVisualBrush = compositor.CreateSurfaceBrush(childVisualSurface);
            childVisualBrush.HorizontalAlignmentRatio = 0;
            childVisualBrush.VerticalAlignmentRatio = 0;
            childVisualBrush.Stretch = CompositionStretch.None;

            redirectVisual = compositor.CreateSpriteVisual();
            redirectVisual.RelativeSizeAdjustment = Vector2.One;
            redirectVisual.Brush = childVisualBrush;

            if (childVisualBrushOffsetEnabled)
            {
                offsetBind = compositor.CreateExpressionAnimation("Vector2(visual.Offset.X, visual.Offset.Y)");
            }

            this.Loaded += RedirectVisualView_Loaded;
            this.Unloaded += RedirectVisualView_Unloaded;
            RegisterPropertyChangedCallback(PaddingProperty, OnPaddingPropertyChanged);
        }

        protected virtual bool ChildVisualBrushOffsetEnabled => false;

        protected bool RedirectVisualAttached => attached;

        protected bool RedirectVisualEnabled
        {
            get => redirectVisualEnabled;
            set
            {
                if (redirectVisualEnabled != value)
                {
                    redirectVisualEnabled = value;

                    if (value)
                    {
                        if (IsLoaded)
                        {
                            AttachVisuals();
                        }
                    }
                    else
                    {
                        DetachVisuals();
                    }
                }
            }
        }


        private bool attached;
        private bool redirectVisualEnabled = true;
        private bool childVisualBrushOffsetEnabled;

        private Grid? layoutRoot;
        private ContentPresenter? childPresenter;
        private Grid? childPresenterContainer;
        private Canvas? ChildHost;
        private Canvas? opacityMaskContainer;


        protected Grid? LayoutRoot
        {
            get => layoutRoot;
            private set
            {
                if (layoutRoot != value)
                {
                    var old = layoutRoot;

                    layoutRoot = value;

                    if (old != null)
                    {
                        old.SizeChanged -= LayoutRoot_SizeChanged;
                    }

                    if (layoutRoot != null)
                    {
                        layoutRoot.SizeChanged += LayoutRoot_SizeChanged;
                    }
                }
            }
        }

        protected ContentPresenter? ChildPresenter
        {
            get => childPresenter;
            private set
            {
                if (childPresenter != value)
                {
                    var old = childPresenter;

                    childPresenter = value;

                    if (old != null)
                    {
                        old.SizeChanged -= ChildPresenter_SizeChanged;
                    }

                    if (childPresenter != null)
                    {
                        childPresenter.SizeChanged += ChildPresenter_SizeChanged;
                    }
                }
            }
        }

        protected Grid? ChildPresenterContainer
        {
            get => childPresenterContainer;
            private set
            {
                if (childPresenterContainer != value)
                {
                    childPresenterContainer = value;

                    if (childPresenterContainer != null)
                    {
                        if (!childVisualBrushOffsetEnabled)
                        {
                            if (childPresenterContainer.RowDefinitions.Count > 0)
                            {
                                childPresenterContainer.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Auto);
                            }
                            if (childPresenterContainer.ColumnDefinitions.Count > 0)
                            {
                                childPresenterContainer.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Auto);
                            }
                        }
                    }
                }
            }
        }


        protected Canvas? OpacityMaskContainer
        {
            get => opacityMaskContainer;
            private set => opacityMaskContainer = value;
        }

        private Visual hostVisual;
        private Compositor compositor;

        private CompositionVisualSurface childVisualSurface;
        private CompositionSurfaceBrush childVisualBrush;

        private SpriteVisual redirectVisual;
        private ExpressionAnimation? offsetBind;

        protected CompositionBrush ChildVisualBrush => childVisualBrush;

        protected SpriteVisual RootVisual
        {
            get => redirectVisual;
            set => redirectVisual = value;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DetachVisuals();

            LayoutRoot = GetTemplateChild(nameof(LayoutRoot)) as Grid;
            ChildPresenter = GetTemplateChild(nameof(ChildPresenter)) as ContentPresenter;
            ChildPresenterContainer = GetTemplateChild(nameof(ChildPresenterContainer)) as Grid;
            ChildHost = GetTemplateChild(nameof(ChildHost)) as Canvas;
            OpacityMaskContainer = GetTemplateChild(nameof(OpacityMaskContainer)) as Canvas;

            if (RedirectVisualEnabled)
            {
                AttachVisuals();
            }
        }

        public UIElement? Child
        {
            get { return (UIElement?)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }

        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.Register("Child", typeof(UIElement), typeof(RedirectVisualView), new PropertyMetadata(null));

        private void AttachVisuals()
        {
            if (attached) return;

            attached = true;

            if (LayoutRoot != null)
            {
                if (ChildPresenter != null)
                {
                    var childBorderVisual = ElementCompositionPreview.GetElementVisual(ChildPresenter);

                    childVisualSurface.SourceVisual = childBorderVisual;

                    if (offsetBind != null)
                    {
                        offsetBind.SetReferenceParameter("visual", childBorderVisual);
                        childVisualBrush.StartAnimation("Offset", offsetBind);
                    }
                }

                if (ChildPresenterContainer != null)
                {
                    ElementCompositionPreview.GetElementVisual(ChildPresenterContainer).IsVisible = false;
                }

                if (OpacityMaskContainer != null)
                {
                    ElementCompositionPreview.GetElementVisual(OpacityMaskContainer).IsVisible = false;
                }

                if (ChildHost != null)
                {
                    ElementCompositionPreview.SetElementChildVisual(ChildHost, redirectVisual);
                }

                UpdateSize();
            }

            OnAttachVisuals();
        }

        private void DetachVisuals()
        {
            if (!attached) return;

            attached = false;

            if (LayoutRoot != null)
            {
                childVisualSurface.SourceVisual = null;

                if (offsetBind != null)
                {
                    childVisualBrush.StopAnimation("Offset");
                    offsetBind.ClearAllParameters();
                }

                if (ChildPresenterContainer != null)
                {
                    ElementCompositionPreview.GetElementVisual(ChildPresenterContainer).IsVisible = true;
                }

                if (OpacityMaskContainer != null)
                {
                    ElementCompositionPreview.GetElementVisual(OpacityMaskContainer).IsVisible = true;
                }

                if (ChildHost != null)
                {
                    ElementCompositionPreview.SetElementChildVisual(ChildHost, null);
                }
            }

            OnDetachVisuals();
        }

        private void RedirectVisualView_Unloaded(object sender, RoutedEventArgs e)
        {
            DetachVisuals();
        }

        private void RedirectVisualView_Loaded(object sender, RoutedEventArgs e)
        {
            if (RedirectVisualEnabled)
            {
                AttachVisuals();
            }
        }

        private void OnPaddingPropertyChanged(DependencyObject sender, DependencyProperty dp)
        {
            UpdateSize();
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSize();
        }


        private void ChildPresenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            if (attached && LayoutRoot != null)
            {
                if (ChildPresenter != null)
                {
                    childVisualSurface.SourceSize = new Vector2((float)ChildPresenter.ActualWidth, (float)ChildPresenter.ActualHeight);
                }
            }

            OnUpdateSize();
        }

        protected virtual void OnAttachVisuals()
        {

        }

        protected virtual void OnDetachVisuals()
        {

        }

        protected virtual void OnUpdateSize()
        {

        }
    }
}
