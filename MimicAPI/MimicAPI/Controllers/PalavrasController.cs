﻿using Microsoft.AspNetCore.Mvc;
using MimicAPI.Database;
using MimicAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        public readonly MimicContext _banco;
        public PalavrasController(MimicContext banco)
        {
            _banco = banco; //injeção de dependencia            
        }
        //APP  -- /api/palavras
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas()
        {
            //return new JsonResult(_banco.Palavras);
            return Ok(_banco.Palavras);
        }

        //WEB -- /api/palavras/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = Ok(_banco.Palavras.Find(id));
            if (obj == null) return NotFound();
            return Ok();
        }

        //-- /api/palavras(Post:id,nome,atico,pontuacao,criacao)
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Palavra palavra) {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
            return Ok();
        }

        //-- /api/palavras(Put:id,nome,atico,pontuacao,criacao)
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody] Palavra palavra) {
            palavra.Id = id;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return Ok();
        }

        // -- api/palavras/1 Delete
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id) {
            var palavra = _banco.Palavras.Find(id);
            palavra.Ativo = false;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();
            return Ok();
        }
    }
}
