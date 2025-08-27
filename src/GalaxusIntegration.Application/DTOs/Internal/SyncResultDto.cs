namespace GalaxusIntegration.Application.DTOs.Internal
{
    public class SyncResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}