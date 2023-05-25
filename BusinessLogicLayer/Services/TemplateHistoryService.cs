using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services
{
    public class TemplateHistoryService : ITemplateHistoryService
    {
        private readonly ITemplateHistoryRepository _templateHistoryRepository;
        private readonly IMapper _mapper;

        public TemplateHistoryService(ITemplateHistoryRepository templateHistoryRepository, IMapper mapper)
        {
            _templateHistoryRepository = templateHistoryRepository;
            _mapper = mapper;

        }

        public List<TemplateHistoryDTO> GetAll()
        {
            var data = _templateHistoryRepository.GetAll();

            var mappedData = _mapper.Map<List<TemplateHistoryDTO>>(data);

            mappedData.ForEach(e =>
            {
                e.TemplateDTO = _mapper.Map<TemplateDTO>(data.First(x => x.TemplateHistoryId == e.TemplateHistoryId).Template);
            });

            return mappedData;
        }

        TemplateHistoryDTO ITemplateHistoryService.GetById(Guid id)
        {
            var data = _templateHistoryRepository.GetById(id);

            var mappedData = _mapper.Map<TemplateHistoryDTO>(data);

            mappedData.TemplateDTO = _mapper.Map<TemplateDTO>(data.Template);

            return mappedData;
        }

        List<TemplateHistoryDTO> ITemplateHistoryService.GetHistoricByTemplateId(Guid id)
        {
            return _mapper.Map<List<TemplateHistoryDTO>>(_templateHistoryRepository.GetHistoricByTemplateId(id)) ;
        }

        /* 
         *         CreateTemplateHistoryDTO ITemplateHistoryService.Add(CreateTemplateHistoryDTO templateHistoryDTO)
         {
             var mappedRole = _mapper.Map<Templatehistory>(templateHistoryDTO);

             return _mapper.Map<CreateTemplateHistoryDTO>(_templateHistoryRepository.Add(mappedRole));
         }

         void ITemplateHistoryService.Delete(Guid id)
         {
             _templateHistoryRepository.Delete(id);
         }
        TemplateHistoryDTO ITemplateHistoryService.GetByName(string name)
         {
             var data = _templateHistoryRepository.GetByName(name);

             var mappedData = _mapper.Map<TemplateHistoryDTO>(data);

             mappedData.TemplateDTO = _mapper.Map<TemplateDTO>(data.Template);

             return mappedData;
         }
         TemplateHistoryDTO ITemplateHistoryService.Update(Guid id, TemplateHistoryDTO templateHistoryDTO)
         {
             var mappedData = _mapper.Map<Templatehistory>(templateHistoryDTO);

             return _mapper.Map<TemplateHistoryDTO>(_templateHistoryRepository.Update(id, mappedData));
         }*/


    }
}

