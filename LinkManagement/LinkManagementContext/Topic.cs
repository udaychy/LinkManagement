using System.ComponentModel.DataAnnotations.Schema;

namespace LinkManagement
{
    public partial class Topic
    {
        [NotMapped]
        public int SubTopicCount { get; set; }
    }
}