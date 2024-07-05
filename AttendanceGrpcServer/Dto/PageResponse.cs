namespace AttendanceGrpcServer.Dto
{
    public class PageResponse<T> : Response<List<T>>
    {
        public int TotalRecord { get; set; }
        public int PageIndex { get; set; }

    }
}
