using Notifier.Core.Entities;
using Notifier.Core.Gateways;

namespace Notifier.Core.UseCases
{
    public interface IUnsubscribe
        : IUseCaseInteractor<UnsubscribeRequest, UnsubscribeResponse>
    {}

    public class UnsubscribeInteractor : IUnsubscribe
    {
        IRepositoryGateway<string, Community> _communities;
        public UnsubscribeInteractor(IRepositoryGateway<string, Community> repository)
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