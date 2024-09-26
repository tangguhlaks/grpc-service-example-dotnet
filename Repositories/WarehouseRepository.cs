using GrpcServiceExample.Repositories.db;

namespace GrpcServiceExample.Repositories
{
    public interface IWarehouseRepository
    {
        IWarehouseDb db();
    }
    public class WarehouseRepository : IWarehouseRepository
    {
        private IWarehouseDb _db;
        public WarehouseRepository(IWarehouseDb db)
        {
            _db = db;
        }

        public IWarehouseDb db()
        {
            return _db;
        }
    }
}
