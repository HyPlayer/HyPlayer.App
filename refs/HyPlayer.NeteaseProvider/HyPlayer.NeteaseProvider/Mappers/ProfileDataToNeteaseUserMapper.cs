using HyPlayer.NeteaseApi.Models.ResponseModels;
using HyPlayer.NeteaseProvider.Models;

namespace HyPlayer.NeteaseProvider.Mappers;

public static class ProfileDataToNeteaseUserMapper
{
    public static NeteaseUser MapToNeteaseUser(this UserInfoDto profileData)
    {
        return new NeteaseUser
               {
                   Name = profileData.Nickname!,
                   ActualId = profileData.UserId!,
                   Gender = profileData.Gender ?? -1,
                   BackgroundUrl = profileData.BackgroundUrl,
                   Followed = profileData.Followed is true,
                   VipType = profileData.VipType,
                   AvatarUrl = profileData.AvatarUrl,
                   Description = profileData.Signature
               };
    }
}