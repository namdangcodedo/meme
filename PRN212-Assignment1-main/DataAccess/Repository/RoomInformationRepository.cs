using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public void AddRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.Add(roomInformation);

        public RoomInformation GetRoomInformationByID(int roomId) => RoomInformationDAO.Instance.GetRoomByID(roomId);

        public List<RoomInformation> GetRoomInformationList() => RoomInformationDAO.Instance.GetRoomInformationList().ToList();

        public void RemoveRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.Delete(roomInformation);

        public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.Instance.Update(roomInformation);


        public List<RoomInformation> FindByRoomNumber(string roomNumber)
        {
            var rooms = GetRoomInformationList();
            return rooms.Where(r => r.RoomNumber.Contains(roomNumber)).ToList();
        }
    }
}
