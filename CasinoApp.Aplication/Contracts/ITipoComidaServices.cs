﻿using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoComida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface ITipoComidaServices
    {
        public RequestResult<TipoComidaDto> Update(TipoComidaDto tipoComida);
        public bool Delete(int idTípoComida);

        public RequestResult<List<TipoComidaDto>> GetAll();
        public RequestResult<TipoComidaDto> GetById(int idTipoComida);
        RequestResult<TipoComidaDto> Create(TipoComidaDto tipoComida);
    }
}
