﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusic.API.Resourses;
using MyMusic.API.Validations;
using MyMusic.Core.Models;
using MyMusic.Core.Services;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyMusic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;
        public MusicController(IMusicService musicService, IMapper mapper, IArtistService artistService)
        {
            _musicService = musicService;
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet("")]

        public async Task<ActionResult<IEnumerable<MusicResource>>> GetAllMusics()
        {
            var musics = await _musicService.GetAllWithArtist();
            var musicsResource = _mapper.Map<IEnumerable<Music>, IEnumerable<MusicResource>>(musics);
            return Ok(musicsResource);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<MusicResource>> GetMusicById(int id)
        {
            var music = await _musicService.GetMusicById(id);
            if (music == null)
            {
                return NotFound();
            }

            var musicResource = _mapper.Map<Music, MusicResource>(music);
            return Ok(musicResource);


        }

        [HttpPost]

        public async Task<ActionResult<MusicResource>> CreateMusic([FromBody] SaveResourceMusic musicSaveResource)
        {

            var validatorMusic = new SaveMusicResourceValidator();
            var validationResult = await validatorMusic.ValidateAsync(musicSaveResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var music = _mapper.Map<SaveResourceMusic, Music>(musicSaveResource);
            var newMusic = await _musicService.CreateMusic(music);
            return Ok(newMusic);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<MusicResource>> UpdateMusic(int id, [FromBody] SaveResourceMusic updateSaveResource)
        {
            var validator = new SaveMusicResourceValidator();
            var resultValidato = await validator.ValidateAsync(updateSaveResource);

            if (!resultValidato.IsValid)
            {
                return BadRequest(resultValidato.Errors);

            }
            var musicToBeUpdate = await _musicService.GetMusicById(id);

            if (musicToBeUpdate == null)
            {
                return NotFound();
            }
            var musicUpdate = _mapper.Map<SaveResourceMusic, Music>(updateSaveResource);
            await _musicService.UpdateMusic(musicToBeUpdate, musicUpdate);

            var musicNewUpdate = await _musicService.GetMusicById(id);
            var musicUpdateResource = _mapper.Map<Music, MusicResource>(musicNewUpdate);
            return Ok(musicUpdateResource);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteMusic(int id)
        {
            var music = await _musicService.GetMusicById(id);
            if (music == null)
            {
                return NotFound();
            }
            await _musicService.DeleteMusic(music);

            return NoContent();

        }
        [HttpGet("Artist/id")]

        public async Task<ActionResult<IEnumerable<MusicResource>>>  GetAllMusicsByArtistId(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            if(artist == null)
            {
                return NotFound();
            }
            var musics = await _musicService.GetMusicsByArtistId(id);
            var musicResources = _mapper.Map<IEnumerable<Music>,IEnumerable<MusicResource>> (musics);
            return Ok(musicResources);
        }


    }
}

