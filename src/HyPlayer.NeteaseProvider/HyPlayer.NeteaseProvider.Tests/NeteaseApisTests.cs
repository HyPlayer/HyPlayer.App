using FluentAssertions;
using HyPlayer.NeteaseApi;
using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.NeteaseProvider.Tests;

public class NeteaseApisTests : IAsyncLifetime
{
    private readonly NeteaseProvider _provider = new();

    //[Theory]
    public async Task LoginEmail_Should_LoginWithInfo(string email, string password)
    {
        var result = await _provider.RequestAsync(NeteaseApis.LoginEmailApi,
                                                  new LoginEmailRequest
                                                  {
                                                      Email = email,
                                                      Password = password
                                                  });
        result.Match(
            (resp) =>
            {
                resp.Should().NotBeNull();
                resp.Profile?.Gender.Should().BeOneOf(0, 1);
                return true;
            },
            (err) => throw err
        );
    }

    //[Theory]
    public async Task LoginCellphone_Should_LoginWithInfo(string phone, string password)
    {
        var result =
            await _provider.RequestAsync(NeteaseApis.LoginCellphoneApi,
                                         new LoginCellphoneRequest()
                                         {
                                             Cellphone = phone,
                                             Password = password
                                         });
        result.Match(
            (resp) =>
            {
                resp.Should().NotBeNull();
                resp.Profile?.Gender.Should().BeOneOf(0, 1);
                return true;
            },
            (err) => throw err
        );
    }

    [Theory]
    [InlineData("2034742057")]
    public async void SongDetail_Single_Should_HasInfo(string id)
    {
        var result =
            await _provider.RequestAsync(
                NeteaseApis.SongDetailApi,
                new SongDetailRequest()
                {
                    Id = id
                });
        result.Match(
            success =>
            {
                success.Songs.Should().HaveCount(1);
                success.Songs?[0].Id.Should().Be(id);
                return true;
            },
            error => throw error
        );
    }

    [Theory]
    [InlineData("2034742057", "1811209786", "1953828605")]
    public async void SongDetail_Multiple_Should_HasInfo(params string[] ids)
    {
        var result =
            await _provider.RequestAsync(
                NeteaseApis.SongDetailApi,
                new SongDetailRequest()
                {
                    IdList = ids.ToList()
                });
        result.Match(
            success =>
            {
                success.Songs.Should().HaveCount(3);
                return true;
            },
            error => throw error
        );
    }

    [Theory]
    [InlineData("2034742057")]
    public async void SongUrl_Single_Should_ReturnUrl(string id)
    {
        var result =
            await _provider.RequestAsync(
                NeteaseApis.SongUrlApi,
                new SongUrlRequest()
                {
                    Id = id,
                    Level = "jymaster"
                });
        result.Match(
            success =>
            {
                success.SongUrls.Should().NotBeNull();
                success.SongUrls.Should().HaveCount(1);
                success.SongUrls![0].Url.Should().NotBeEmpty();
                return true;
            },
            error => throw error
        );
    }

    [Theory]
    [InlineData("2034742057", "1811209786", "1953828605")]
    public async void SongUrl_Multiple_Should_ReturnUrl(params string[] ids)
    {
        var result =
            await _provider.RequestAsync(
                NeteaseApis.SongUrlApi,
                new SongUrlRequest()
                {
                    IdList = ids.ToArray(),
                    Level = "jymaster"
                });
        result.Match(
            _ => true,
            error => throw error
        );
    }

    [Theory]
    [InlineData("2778408564")]
    [InlineData("897784673")]
    public async void PlaylistDetail_Should_ReturnNormal(string playlistId)
    {
        var result = await _provider.RequestAsync(
            NeteaseApis.PlaylistDetailApi,
            new PlaylistDetailRequest()
            {
                Id = playlistId
            });
        result.Match(
            _ => true,
            error => throw error
        );
    }

    [Theory]
    [InlineData("2778408564")]
    [InlineData("897784673")]
    public async void PlaylistTracksGet_Should_ReturnNormal(string playlistId)
    {
        var result = await _provider.RequestAsync(
            NeteaseApis.PlaylistTracksGetApi,
            new PlaylistTracksGetRequest()
            {
                Id = playlistId
            });
        result.Match(
            success => success.Playlist.TrackIds.Should().NotBeEmpty(),
            error => throw error
        );
    }

    [Theory]
    [InlineData("1455706958")]
    [InlineData("1880520974")]
    public async void Lyric_Should_ReturnNormal(string songId)
    {
        var result = await _provider.RequestAsync(
            NeteaseApis.LyricApi,
            new LyricRequest()
            {
                Id = songId
            }
        );
        result.Match(
            success => success?.Lyric?.Lyric.Should().NotBeEmpty(),
            error => throw error
        );
    }

    [Fact]
    public async void Toplist_Should_ReturnNormal()
    {
        var result = await _provider.RequestAsync(NeteaseApis.ToplistApi, new ToplistRequest());
        result.Match(
            success => success.List.Should().NotBeEmpty(),
            error => throw error
        );
    }

    [Fact]
    public async void RecommendedPlaylist_Should_ReturnNormal()
    {
        var result = await _provider.RequestAsync(NeteaseApis.RecommendSongsApi, new RecommendSongsRequest());
        result.Match(
            success => success.Data?.DailySongs.Should().NotBeEmpty(),
            error => throw error
        );

        (await (await _provider.GetRecommendationAsync(NeteaseTypeIds.Playlist))
               .Should().BeAssignableTo<LinerContainerBase>().Subject
               .GetAllItemsAsync()).Should().NotBeEmpty();
    }

    [Fact]
    public async void Search_Song_Should_ReturnNormal()
    {
        var result = await _provider.SearchProvidableItemsAsync("spiral", NeteaseTypeIds.SingleSong);
        (await result.Should().BeAssignableTo<LinerContainerBase>().Subject.GetAllItemsAsync()).Should().NotBeEmpty();
    }

    [Fact]
    public async void PlaylistCategoryListApi_Should_BeNormal()
    {
        var result = await _provider.RequestAsync(NeteaseApis.PlaylistCategoryListApi, new PlaylistCategoryListRequest
                                                      {
                                                          Category = "ACG",
                                                          Limit = 15
                                                      });
        result.Match(s => s.Playlists.Should().NotBeEmpty(),
                     e => throw e);
    }

    [Fact]
    public async void AiDjRcmdInfo_Should_ReturnNormal()
    {
        var result = await _provider.RequestAsync(NeteaseApis.AiDjContentRcmdInfoApi, new AiDjContentRcmdInfoRequest());
        result.Match(success =>
                         success.Code.Should().Be(200),
                     e => throw e);
    }

    [Theory]
    [InlineData("1972641406")]
    public async void ListenFirstInfo_Should_BeNormal(string songId)
    {
        var result = await _provider.RequestAsync(NeteaseApis.MusicFirstListenInfoApi, new MusicFirstListenInfoRequest()
                                                      {
                                                          SongId = songId
                                                      });
        result.Match(s => s.Code.Should().Be(200),
                     e => throw e);
    }
    
    [Theory]
    [InlineData("DEFAULT")]
    [InlineData("SCENE_RCMD", "NIGHT_EMO")]
    public async void PersonalFm_Should_BeNormal(string mode, string? subMode = null)
    {
        var result = await _provider.RequestAsync(NeteaseApis.PersonalFmApi, new PersonalFmRequest
                                                                             {
                                                                                 Mode = mode,
                                                                                 SubMode = subMode,
                                                                                 Limit = 5
                                                                             });
        result.Match(s => s.Code.Should().Be(200),
                     e => throw e);
    }

    public async Task InitializeAsync()
    {
        _provider.Option.Cookies["__csrf"] = Secrets.Csrf;
        _provider.Option.Cookies["MUSIC_U"] = Secrets.MUSIC_U;
    }

    public async Task DisposeAsync()
    {
    }
}