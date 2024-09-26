using Grpc.Core;
using GrpcServiceExample.Protos;
using GrpcServiceExample.UseCase;
using static GrpcServiceExample.Protos.WarehouseService;

namespace GrpcServiceExample.Services
{
    public class WarehouseService : WarehouseServiceBase
    {
        private IWarehouseUseCase _useCase;
        public WarehouseService(IWarehouseUseCase useCase)
        {
            _useCase = useCase;
        }

        public override async Task<WarehouseResponse> Add(WarehouseModel req, ServerCallContext context)
        {
            try
            {
                // Simulate adding to the warehouse
                await _useCase.Add(req, context); // Pastikan method ini tidak comment
                return new WarehouseResponse { Message = "Add Successfully" };
            }
            catch (Exception ex)
            {
                // Log error message
                Console.WriteLine($"Error occurred: {ex.Message}");
                return new WarehouseResponse { Message = $"Error: {ex.Message}" };
            }
        }

        public override async Task<WarehouseResponseGetList> GetList(Empty o, ServerCallContext context)
        {
            var ret = new WarehouseResponseGetList();
            try
            {
                ret = await _useCase.GetList(o, context);
                return ret;
            }
            catch (Exception ex)
            {
                // Log error message
                Console.WriteLine($"Error occurred: {ex.Message}");
                return ret;
            }
        }

        public override async Task<WarehouseResponseGetById> GetById(WarehouseGetReq o, ServerCallContext context)
        {
            var ret = new WarehouseResponseGetById();
            try
            {
                ret = await _useCase.GetById(o, context);
                return ret;
            }
            catch (Exception ex)
            {
                // Log error message
                Console.WriteLine($"Error occurred: {ex.Message}");
                return ret;
            }
        }

    }
}
