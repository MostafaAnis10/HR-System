 namespace HR_System.BLL.ResponseResult
{
    public record Response <T> (T result, string errorMassage, bool IsHaveErrorOrNo);

}
