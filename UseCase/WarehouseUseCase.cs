using AutoMapper;
using Grpc.Core;
using GrpcServiceExample.Model;
using GrpcServiceExample.Protos;
using GrpcServiceExample.Repositories;
using Org.BouncyCastle.Crypto;

namespace GrpcServiceExample.UseCase
{
    public interface IWarehouseUseCase
    {
        Task<Protos.WarehouseResponse> Add(Protos.WarehouseModel o,ServerCallContext context);
        Task<Protos.WarehouseResponseGetList> GetList(Protos.Empty o, ServerCallContext context);
    }
    public class WarehouseUseCase : IWarehouseUseCase
    {
        private IWarehouseRepository _repository;
        private readonly IMapper _mapper;

        public WarehouseUseCase(IWarehouseRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        Task<WarehouseResponse> IWarehouseUseCase.Add(WarehouseModel o, ServerCallContext context)
        {
            try
            {
                var oNew = _mapper.Map<Warehouse>(o);
                var res = _repository.db().CreateWarehouse(oNew);
                return Task.FromResult(new WarehouseResponse { Message = "Success" });
            }
            catch (Exception ex) {
                throw;
            }
        }

        Task<WarehouseResponseGetList> IWarehouseUseCase.GetList(Empty o, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}
