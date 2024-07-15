using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomInformationRepository
    {
        List<RoomInformation> GetRoomInformationList();
        RoomInformation GetRoomInformationByID(int roomId);
        void AddRoomInformation(RoomInformation roomInformation);
        void RemoveRoomInformation(RoomInformation roomInformation);
        void UpdateRoomInformation(RoomInformation roomInformation);

        List<RoomInformation> FindByRoomNumber(string roomNumber);
    }
}
