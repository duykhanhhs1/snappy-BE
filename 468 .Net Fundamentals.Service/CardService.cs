﻿using _468_.Net_Fundamentals.Domain;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using _468_.Net_Fundamentals.Domain.EnumType;

namespace _468_.Net_Fundamentals.Service
{
    public class CardService : RepositoryBase<Card>, ICardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CardService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task Create(CardCreateVM request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var business = await _unitOfWork.Repository<Business>().FindAsync(request.BusId);
                                
                var card = new Card
                {
                    Name = request.Name,
                    Business = business
                };
                await _unitOfWork.Repository<Card>().InsertAsync(card);
                               
                
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e) 
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<CardVM>> GetAllByBusiness(int busId)
        {
            var cardVMs = await _unitOfWork.Repository<Card>()
                .Query()
                .Where(_ => _.BusinessId == busId)
                .Select(card => new CardVM
                {
                    /*Id = card.Id,*/
                    Name = card.Name,
                    Description = card.Description,
                    Duedate = card.Duedate,
                    Priority = card.Priority,
                    BusinessId = card.BusinessId,
                })
                .ToListAsync();

            return cardVMs;
        }

        public async Task<CardVM> GetDetail(int id)
        {
            var cardVM = await _unitOfWork.Repository<Card>()
                .Query()
                .Where(_ => _.Id == id)
                .Select(card => new CardVM
                {
                    Id = card.Id,
                    Name = card.Name,
                    Description = card.Description,
                    Duedate = card.Duedate,
                    Priority = card.Priority,
                    BusinessId = card.BusinessId,
                }).FirstOrDefaultAsync();

            return cardVM;
        }

         public async Task Delete(int id)
        {   
            await _unitOfWork.Repository<Card>().DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();    
        }

        public async Task UpdateName(int id, string newName)
        {
            var card =  await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Name = newName;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePriority(int id, TaskPriority newPriority)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Priority = newPriority;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateBusiness(int id, int newBusinessId)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.BusinessId = newBusinessId;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDescription(int id, string newDescription)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Description = newDescription;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDuedate(int id, DateTime newDuedate)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);

            card.Duedate = newDuedate;

            await _unitOfWork.SaveChangesAsync();
        }

        // TAG

        public async Task AddTagOnCard(int id, int tagId)
        {
            var card = await _unitOfWork.Repository<Card>().FindAsync(id);
            var tag = await _unitOfWork.Repository<Tag>().FindAsync(tagId);

            var cardTag = new CardTag
            {
                Card = card,
                Tag = tag
            };

            await _unitOfWork.Repository<CardTag>().InsertAsync(cardTag);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTagOnCard(int id, int tagId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var cardTag = await _unitOfWork.Repository<CardTag>()
                    .Query()
                    .Where(_ => _.CardId == id && _.TagId == tagId)
                    .FirstOrDefaultAsync();

                await _unitOfWork.Repository<CardTag>().DeleteAsync(cardTag);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<CardTagVM>> GetAllTagOnCard(int id)
        {
            var cardTagVMs = await _unitOfWork.Repository<CardTag>()
                    .Query()
                    .Where(_ => _.CardId == id)
                    .Select(cardTag => new CardTagVM
                    {
                        CardId = cardTag.CardId,
                        TagId = cardTag.TagId,
                        TagName = cardTag.Tag.Name
                    })
                    .ToListAsync();

            return cardTagVMs;
        }
    }
}
