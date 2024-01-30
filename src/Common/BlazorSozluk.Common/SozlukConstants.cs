using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common;

public class SozlukConstants
{
    public const string RabbitMQHost = "localhost";
    public const string DefaultExchangeType = "direct";

    public const string UserExchangeName = "UserExchange";
    public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

    public const string FavExchangeName = "FavExchangeName";
    public const string CreateEntryFavQueueName = "CreateEntryFavQueueName";
    public const string DeleteEntryFavQueueName = "DeleteEntryFavQueueName";
    public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueueName";
    public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueueName";

    public const string VoteExchangeName = "VoteExchangeName";
    public const string CreateEntryVoteQueueName = "CreateEntryVoteQueueName";
    public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueueName";
    public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueueName";
    public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueueName";
}
