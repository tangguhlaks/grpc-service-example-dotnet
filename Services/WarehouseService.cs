using Grpc.Core;
using GrpcServiceExample.Protos;
using GrpcServiceExample.UseCase;

namespace GrpcServiceExample.Services
{
    public class WarehouseService : GrpcServiceExample.Protos.WarehouseService.WarehouseServiceBase
    {
        private IWarehouseUseCase _useCase;
        public WarehouseService(IWarehouseUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<WarehouseResponse> Add(WarehouseModel req,ServerCallContext context)
        {
            try
            {
                _ = _useCase.Add(req, context);
                return new WarehouseResponse { Message = "Add Successfuly" };
            }catch (Exception ex)
            {
                return new WarehouseResponse { Message = ex.Message };
            }
        }
    }
}
