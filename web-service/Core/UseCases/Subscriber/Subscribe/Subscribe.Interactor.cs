using Notifier.Core.Entities;
using Notifier.Core.Interfaces;

namespace Notifier.Core.UseCases
{
    public class SubscribeInteractor
        : IUseCaseInteractor<SubscribeRequest, SubscribeResponse>
    {
        IRepository<string, Community> _communities;
        public SubscribeInteractor(IRepository<string, Community> repository)
        {
            _communities = repository;
        }

        public SubscribeResponse Handle(SubscribeRequest request)
        {
            var community = _communities.Get(request.CommunityId);
            community.Subscribers.Add(request.PhoneNumber);
            _communities.Update(community);

            return new SubscribeResponse();
        }
    }
}