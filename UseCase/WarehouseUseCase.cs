using AutoMapper;
using Grpc.Core;
using GrpcServiceExample.Model;
using GrpcServiceExample.Protos;
using GrpcServiceExample.Repositories;
using GrpcServiceExample.Repositories.db;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto;

namespace GrpcServiceExample.UseCase
{
    public interface IWarehouseUseCase
    {
        Task<Protos.WarehouseResponse> Add(Protos.WarehouseModel o,ServerCallContext context);
        Task<Protos.WarehouseResponseGetList> GetList(Protos.Empty o, ServerCallContext context);
        Task<Protos.WarehouseResponseGetById> GetById(Protos.WarehouseGetReq o, ServerCallContext context);
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
                return Task.FromResult(new WarehouseResponse { Message = "Failed " });
            }
        }

        Task<WarehouseResponseGetList> IWarehouseUseCase.GetList(Empty o, ServerCallContext context)
        {
            var res = _repository.db().GetWarehouse();
            var warehouseList = _mapper.Map<IEnumerable<WarehouseModel>>(res);
            var response = new WarehouseResponseGetList();
            response.Warehouses.AddRange(warehouseList);
            return Task.FromResult(response);
        }


        Task<WarehouseResponseGetById> IWarehouseUseCase.GetById(WarehouseGetReq o, ServerCallContext context)
        {
            var res = _repository.db().GetWarehouseById(o.Id);
            if (res == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Warehouse with ID {o.Id} not found"));
            }
            var warehouse = _mapper.Map<WarehouseModel>(res);
            return Task.FromResult(new WarehouseResponseGetById { Warehouses =warehouse });
        }
    }
}
