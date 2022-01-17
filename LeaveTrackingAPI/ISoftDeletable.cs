namespace LeaveTrack
{
    public interface ISoftDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not a record has been flagged as deleted.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
