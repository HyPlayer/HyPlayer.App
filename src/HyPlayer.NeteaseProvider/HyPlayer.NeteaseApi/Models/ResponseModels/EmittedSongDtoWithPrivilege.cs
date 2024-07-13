using System.Text.Json.Serialization;
using HyPlayer.NeteaseApi.ApiContracts;

namespace HyPlayer.NeteaseApi.Models.ResponseModels;

public class EmittedSongDtoWithPrivilege : EmittedSongDto
{
    [JsonPropertyName("privilege")] public PrivilegeDto? Privilege { get; set; }
}