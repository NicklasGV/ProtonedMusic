//API
global using ProtonedMusicAPI.Database.Entities;
global using ProtonedMusicAPI.DTO.ProductDTO;
global using ProtonedMusicAPI.DTO.CategoryDTO;
global using ProtonedMusicAPI.Database;
global using ProtonedMusicAPI.Interfaces;
global using ProtonedMusicAPI.Repositories;
global using ProtonedMusicAPI.Services;
global using ProtonedMusicAPI.Interfaces.IUser;
global using ProtonedMusicAPI.DTO.UserDTO;
global using ProtonedMusicAPI.DTO.LoginDTO;
global using ProtonedMusicAPI.Helper;
global using ProtonedMusicAPI.Authentication;
global using ProtonedMusicAPI.DTO.ImageDTO;
global using ProtonedMusicAPI.DTO.NewsDTO;
global using ProtonedMusicAPI.DTO.EventDTO;
global using ProtonedMusicAPI.DTO.FrontpageDTO;
global using ProtonedMusicAPI.Interfaces.IFrontpage;
global using ProtonedMusicAPI.Interfaces.IMusic;

//Microsoft
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.OpenApi.Models;

//System
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;