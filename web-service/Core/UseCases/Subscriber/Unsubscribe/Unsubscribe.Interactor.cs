using Notifier.Core.Interfaces;
using Notifier.Core.Models;

namespace Notifier.Core.UseCases
{
    public class UnsubscribeInteractor
        : IUseCaseInteractor<UnsubscribeRequest, UnsubscribeResponse>
    {
        IRepository<string, Community> _communities;
        public UnsubscribeInteractor(IRepository<string, Community> repository)
        {
            _communities = repository;
        }
        public UnsubscribeResponse Handle(UnsubscribeRequest request)
        {
            var community = _communities.Get(request.CommunityId);
            community.Subscribers.Remove(request.PhoneNumber);
            _communities.Update(community);

            return new UnsubscribeResponse();
        }
    }
}