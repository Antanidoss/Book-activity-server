namespace BookActivity.Domain.Models
{
    public class ResponseOpinion : BaseEntity
    {
        /// <summary>
        /// Opinion response type
        /// </summary>
        public ResponseOpinionType ResponseOpinionType { get; set; }

        /// <summary>
        /// Relation of response opinion with the book opinion
        /// </summary>
        public BookOpinion BookOpinion { get; private set; }
        public int BookOpinionId { get; private set; }

        /// <summary>
        /// Relation of response opinion with the user
        /// </summary>
        public AppUser User { get; private set; }
        public int UserId { get; private set; }

        protected ResponseOpinion() : base() { }
        public ResponseOpinion(ResponseOpinionType responseOpinionType, int userId, int bookOpinionId, bool isPublic) : base(isPublic)
        {
            ResponseOpinionType = responseOpinionType;
            UserId = userId;
            BookOpinionId = bookOpinionId;
        }
    }

    public enum ResponseOpinionType
    {
        AgreeingOpinion,
        DisagreeOpinion
    }
}
