using Application.Services.ApiPublisher.DtoModels.Test.Requests;
using Application.Services.ApiPublisher.DtoModels.Test.Responses;
using MediatR;

namespace Application.Services.ApiPublisher.Queries.Test
{
    public class GetTestQueryHandler : IRequestHandler<DtoGetTestRequest, DtoGetTestResponse>
    {
        public async Task<DtoGetTestResponse> Handle(DtoGetTestRequest request, CancellationToken cancellationToken)
        {
            return new DtoGetTestResponse();
        }
    }
}
