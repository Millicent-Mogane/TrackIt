namespace TrackItApp.Models
{
    // ViewModel used for changing a user's password
    public class ChangePasswordViewModel
    {
        // The user's current password (used to verify identity before change)
        public string OldPassword { get; set; }

        // The new password the user wants to set
        public string NewPassword { get; set; }

        // A confirmation field to ensure the user typed the new password correctly
        public string ConfirmPassword { get; set; }
    }
}
