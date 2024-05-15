namespace HouseRenting.Common.Dto.Request;

public record GetRequestsListResponseDto
{
    public List<GetRequestResponseDto> Requests { get; set; } = new();
}
