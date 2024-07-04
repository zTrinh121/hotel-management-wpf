using BusinessObjects;
using Repositories;
using System.Threading.Tasks;

namespace Sevices
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService()
        {
            _roomTypeRepository = new RoomTypeRepository();
        }
        public List<RoomType> GetAll() => _roomTypeRepository.GetAll();
    }
}
