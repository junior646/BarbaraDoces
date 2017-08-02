using DAL;
using INFO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLNovidades
    {
        public List<NovidadesINFO> busObterNovidades()
        {
            //Retorno das Novidades do Site
            try
            {
                DALNovidades DALL = new DALNovidades();
                return DALL.dbRetornarNovidades();
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
