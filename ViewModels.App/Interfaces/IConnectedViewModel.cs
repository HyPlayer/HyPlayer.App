using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.App.Interfaces;

public interface IConnectedViewModel
{
    /// <summary>
    /// 编号
    /// </summary>
    int? ConnectedItemIndex { get; set; }
    /// <summary>
    /// 元素名称
    /// </summary>
    string ConnectedElementName { get; set; }
}

