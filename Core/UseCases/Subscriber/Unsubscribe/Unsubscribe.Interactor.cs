using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

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
            var success = community.Subscribers.Remove(request.PhoneNumber);
            if (success)
            {
                _communities.Update(community);
            }

            return new UnsubscribeResponse();
        }
    }
}