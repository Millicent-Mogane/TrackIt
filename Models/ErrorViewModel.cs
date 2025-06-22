namespace TrackIt.Models
{
    // ViewModel used to represent error details for display in the Error view
    public class ErrorViewModel
    {
        // Unique identifier for the current HTTP request
        public string? RequestId { get; set; }

        // Returns true if the RequestId is not null or empty
        // Used to conditionally display the RequestId in the view
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
