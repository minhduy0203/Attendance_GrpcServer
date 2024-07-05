using AttendanceGrpcServer.Utils;

namespace AttendanceGrpcServer.Dto
{
    public class PageRequest
    {
        private const string DEFAULT_SORT = "Id";
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        //sort property
        public string Property { get; set; } = DEFAULT_SORT;
        //ASC - DESC
        public Constants.Direction Direction { get; set; } = Constants.Direction.ASC;
    }
}
