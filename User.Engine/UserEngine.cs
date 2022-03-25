using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Common.Response;
using User.Model.DTOs;
using User.Model.Interfaces.Engine;
using User.Model.Interfaces.Repos;
using User.Model.Models;

namespace User.Engine
{
    public class UserEngine : IUserEngine
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UserEngine(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public EntityResponse Create(UserEntityDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<UserEntity>(userDTO);
                _userRepository.Add(user);

                return EntityResponse.Create(System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return EntityResponse.Create(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }

        }

        public EntityResponse CreateList(List<UserEntityDTO> userEntities)
        {
            try
            {
                var users = _mapper.Map<List<UserEntity>>(userEntities);
                _userRepository.AddRange(users);

                return EntityResponse.Create(System.Net.HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return EntityResponse.Create(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        public EntityResponse GetUserById(int id)
        {
            try
            {
                var user = _userRepository.FindById(id);

                if (user != null)
                    return EntityResponse.Create(System.Net.HttpStatusCode.OK, user);


                return EntityResponse.Create(System.Net.HttpStatusCode.NotFound, null, "User not found");
            }
            catch (Exception ex)
            {
                return EntityResponse.Create(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        public EntityResponse GetUsers()
        {
            try
            {
                var users = _userRepository.FindAll().Skip(5).Take(5);
                return EntityResponse.Create(System.Net.HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                return EntityResponse.Create(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        public EntityResponse UpdateUser(UserEntityDTO userDTO, int id)
        {
            try
            {
                var user = _userRepository.FindById(id);

                if (user != null)
                {
                    user.FirstName = userDTO.FirstName;
                    user.LastName = userDTO.LastName;
                    user.Email = userDTO.Email;
                    user.Avatar = userDTO.Avatar;

                    _userRepository.Update(user);
                    return EntityResponse.Create(System.Net.HttpStatusCode.OK);
                }

                return EntityResponse.Create(System.Net.HttpStatusCode.NotModified, null, "User not found");
            }
            catch (Exception ex)
            {
                return EntityResponse.Create(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }
    }
}
