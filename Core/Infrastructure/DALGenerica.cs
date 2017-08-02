using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DAL
{
    public class DALGenerica
    {
        public static SqlConnection conexao = new SqlConnection();

        public static void AbrirConexao()
        {
            conexao.ConnectionString = @"Data Source=DDESSBC-33553\SQLEXPRESS;Initial Catalog=DocesBarbara;Persist Security Info=True;Password=acer.77890;User ID=edson.batista;";
            conexao.Open();
        }

        public static void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close(); conexao.Dispose();
            }
        }
        public static void ExecutarComando(string textoComando, List<IDataParameter> parametros)
        {
            if (conexao.State != ConnectionState.Open)
            {
                AbrirConexao();
            }
            SqlCommand comando = new SqlCommand();
            comando.CommandText = textoComando;
            comando.Connection = conexao;
            if (parametros != null) { comando.Parameters.Add(parametros); }

            int registrosAfetados = comando.ExecuteNonQuery();
        }
        public static SqlDataReader SelecionarRegistros(string textoComando, List<IDataParameter> parametros)
        {
            try
            {
                if (conexao.State != ConnectionState.Open)
                {
                    AbrirConexao();
                }
                SqlCommand comando = new SqlCommand();
                comando.CommandText = textoComando;
                comando.Connection = conexao;

                if (parametros != null)
                    if (parametros.Count > 0)
                    {
                        foreach (IDataParameter lObjParam in parametros)
                        {
                            comando.Parameters.Add(lObjParam);
                        }
                    }

                return comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        public static void FecharDataReader(IDataReader pObjDataReader)
        {
            try
            {
                if (pObjDataReader is DBNull)
                {
                    return;
                }
                else
                {
                    if (pObjDataReader.IsClosed)
                    {
                        return;
                    }
                    else
                    {
                        pObjDataReader.Close();
                        pObjDataReader.Dispose();
                    }

                    pObjDataReader = null;
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { }
        }

        public static SqlDataReader SelecionarUmRegistro(string textoComando, List<IDataParameter> parametros)
        {
            if (conexao.State != ConnectionState.Open)
            {
                AbrirConexao();
            }
            SqlCommand comando = new SqlCommand();
            comando.CommandText = textoComando;
            comando.Connection = conexao;


            if (parametros != null || parametros.Count > 0)
            {
                foreach (IDataParameter lObjParam in parametros)
                {
                    comando.Parameters.Add(lObjParam);
                }
            }

            return (SqlDataReader)comando.ExecuteScalar();
        }

        #region "Parametros"

        public static IDataParameter dbParametroVarchar(string pStrNomeParam, string pStrValor)
        {
            IDataParameter lObjParam;
            try
            {
                lObjParam = new SqlParameter(pStrNomeParam, SqlDbType.VarChar);
                lObjParam.Value = pStrValor;
                return lObjParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IDataParameter dbParametroInteger(string pStrNomeParam, int pStrValor)
        {
            IDataParameter lObjParam;
            try
            {
                lObjParam = new SqlParameter(pStrNomeParam, SqlDbType.Int);
                lObjParam.Value = pStrValor;
                return lObjParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IDataParameter dbParametroFloat(string pStrNomeParam, float pStrValor)
        {
            IDataParameter lObjParam;
            try
            {
                lObjParam = new SqlParameter(pStrNomeParam, SqlDbType.Float);
                lObjParam.Value = pStrValor;
                return lObjParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IDataParameter dbParametroBool(string pStrNomeParam, bool pStrValor)
        {
            IDataParameter lObjParam;
            try
            {
                lObjParam = new SqlParameter(pStrNomeParam, SqlDbType.Bit);
                lObjParam.Value = pStrValor;
                return lObjParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IDataParameter dbParametroDate(string pStrNomeParam, DateTime pStrValor)
        {
            IDataParameter lObjParam;
            try
            {
                lObjParam = new SqlParameter(pStrNomeParam, SqlDbType.Date);
                lObjParam.Value = pStrValor;
                return lObjParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IDataParameter dbParametroImage(string pStrNomeParam, DateTime pStrValor)
        {
            IDataParameter lObjParam;
            try
            {
                lObjParam = new SqlParameter(pStrNomeParam, SqlDbType.Image);
                lObjParam.Value = pStrValor;
                return lObjParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}