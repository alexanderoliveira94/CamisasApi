using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CamisasApi.Models;
using CamisasApi.Models.Enuns;
using System.Linq;
using CamisasApi.Data;
using Microsoft.EntityFrameworkCore;
using System;


namespace CamisasApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class CamisasController : ControllerBase

    {
        private readonly DataContext _context;

        public CamisasController(DataContext context)

        {
            _context = context;
        }
        //codigo aqui
        private static List<Camisas> Camisas = new List<Camisas>()
        {
            //MODO DE CRIAR E INCLUIR OBJETOS DE UMA VEZ SÓ NA LISTA
            new Camisas() { Id = 1, Nome = "Corinthians", Valor=500, Tamanho="GG",  Classe=ClasseEnum.Boa },
            new Camisas() { Id = 2, Nome = "Palmeiras", Valor=50, Tamanho="P",  Classe=ClasseEnum.Malhada },
            new Camisas() { Id = 3, Nome = "Vasco", Valor=150, Tamanho="G",  Classe=ClasseEnum.Perfeita },
            new Camisas() { Id = 4, Nome = "São Paulo", Valor=250, Tamanho="M",  Classe=ClasseEnum.Boa },
            new Camisas() { Id = 5, Nome = "Santos", Valor=70, Tamanho="GG",  Classe=ClasseEnum.Malhada },

        };

        [HttpGet("Get")]
        public IActionResult GetFirst()
        {

            return Ok(Camisas[0]);
        }

        [HttpGet("GetAll")] //GET ALL PARA LISTAR TODAS AS CAMISAS DO BANCO DE DADOS
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Camisas> lista = await _context.Camisas.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")] //Filtrar pelo Id as informações de dentro do BANCO DE DADOS
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Camisas p = await _context.Camisas.FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // Adicionar uma nova Camisa exceto se ela não atender as necessidades do código, nesse caso valor menor ou igual a 0.
        public async Task<IActionResult> Add(Camisas novaCamisa)
        {
            try
            {
                if (novaCamisa.Valor <= 0)
                {
                    throw new Exception("Valor da Camisa não pode ser igual ou menor a 0");
                }

                else if (string.IsNullOrEmpty(novaCamisa.Nome))
                {
                    throw new Exception("O nome da Camisa não pode ser vazio ou nulo");
                }
                
                else {
                await _context.Camisas.AddAsync(novaCamisa);
                await _context.SaveChangesAsync();

                return Ok(novaCamisa.Id);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // [HttpPost] //Adicionar uma nova Camisa exceto se ela não atender as necessidades do código, nesse caso valor menor ou igual a 0.
        // public async Task<IActionResult> Add(Camisas novaCamisa)
        // {
        //     try
        //     {
        //         if (novaCamisa.Valor <= 0)
        //         {
        //             throw new Exception("Valor da Camisa não poder ser igual ou menor a 0");
        //         }
        //         await _context.Camisas.AddAsync(novaCamisa);
        //         await _context.SaveChangesAsync();

        //         return Ok(novaCamisa.Id);
        //     }
        //     catch (System.Exception ex)
        //     {

        //         return BadRequest(ex.Message);
        //     }
        // }

        [HttpGet("GetOrdenado")]
        public IActionResult GetOrdem()
        {
            List<Camisas> listaFinal = Camisas.OrderBy(p => p.Nome).ToList();
            return Ok(listaFinal);
        }

        [HttpGet("GetContagem")] //TOTAL DE CAMISAS 
        public IActionResult GetQuantidade()
        {
            return Ok("Quantidade de Camisas: " + Camisas.Count);
        }

        [HttpGet("GetSomaValor")] //SEM O PREÇO DAS CAMISAS 
        public IActionResult GetSomaValor()
        {
            return Ok(Camisas.Sum(p => p.Valor));
        }

        [HttpGet("GetSemCondiçao")] //SEM CAMISAS MALHADAS
        public IActionResult GetSemCondicao()
        {
            List<Camisas> listaBusca = Camisas.FindAll(p => p.Classe != ClasseEnum.Malhada);
            return Ok(listaBusca);
        }

        [HttpGet("GetByNomeAproximado/{nome}")]
        public IActionResult GetByNomeAproximado(string nome)
        {
            List<Camisas> listaBusca = Camisas.FindAll(p => p.Nome.Contains(nome));
            return Ok(listaBusca);
        }

        [HttpGet("GetByValor/{Valor}")] //Filtrar pelo Valor
        public IActionResult GetByValor(int Valor)
        {
            return Ok(Camisas.FirstOrDefault(x => x.Valor == Valor));
        }


        [HttpPut] //Para Editar uma caracteristica especifica da camisa nessa caso valor
        public async Task<IActionResult> Update(Camisas novaCamisa)
        {
            try
            {
                if (novaCamisa.Valor <= 0)
                {
                    throw new Exception("Valor da Camisa não poder ser igual ou menor a 0");
                }
                _context.Camisas.Update(novaCamisa);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")] //Para Deletar uma Camisa
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                Camisas pRemover = await _context.Camisas.FirstOrDefaultAsync(p => p.Id == id);

                _context.Camisas.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetByCondicoes/{enumId}")] //Filtrar pelas condições criadas dentro das Enumerações
        public IActionResult GetByEnum(int enumId)
        {
            //Conversão explicita de int para enum
            ClasseEnum enumDigitado = (ClasseEnum)enumId;

            List<Camisas> listaBusca = Camisas.FindAll(p => p.Classe == enumDigitado);
            return Ok(listaBusca);
        }

        [HttpPut("{id}/UpdateSize")]  //Atualizar o tamanho da camisa // PARA TESTAR NO POSTMAN -- http://localhost:5149/Camisas/{FORNECER ID}/UpdateSize e na descrição todo o conjunto da camisa com o tamanho alterado
        public async Task<IActionResult> UpdateSize(int id, [FromBody] UpdateSizeRequest request)
        {
            try
            {
                Camisas camisa = await _context.Camisas.FindAsync(id);
                if (camisa == null)
                {
                    return NotFound();
                }

                camisa.Tamanho = request.Tamanho;
                await _context.SaveChangesAsync();

                return Ok(camisa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetOrderByPrice")] //Ordenar Por Valor Link o Postman -- http://localhost:porta/Camisas/GetOrderByPrice?order=desc ou asc -- desc se for descresnte e asc se for crescente
        public async Task<IActionResult> GetOrderByPrice(string order = "asc")
        {
            try
            {
                List<Camisas> camisas = order.ToLower() == "desc"
                    ? await _context.Camisas.OrderByDescending(c => c.Valor).ToListAsync()
                    : await _context.Camisas.OrderBy(c => c.Valor).ToListAsync();

                return Ok(camisas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AdicionarFoto/{id}")]
        public async Task<IActionResult> AdicionarFoto(int id, IFormFile foto)
        {
            try
            {
                if (foto == null || foto.Length == 0)
                {
                    return BadRequest("Nenhuma foto foi enviada.");
                }

                // Verifique se o ID é válido e recupere a camisa correspondente do banco de dados
                Camisas camisa = await _context.Camisas.FindAsync(id);

                if (camisa == null)
                {
                    return NotFound();
                }

                // Defina o caminho e o nome do arquivo
                string nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(foto.FileName)}";
                string caminhoArquivo = Path.Combine("D:\\ETEC PROJETOS\\DSI\\CamisasApieMvc 20062023\\Fotos", nomeArquivo);

                // Salve o arquivo no disco
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await foto.CopyToAsync(stream);
                }

                // Atualize a propriedade "Foto" da camisa com o nome do arquivo
                camisa.Foto = nomeArquivo;

                // Salve as alterações no banco de dados
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar foto: {ex.Message}");
            }
        }


    }
}