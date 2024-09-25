namespace sassy.bulk.RequestDto
{
    public class SIgninRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
