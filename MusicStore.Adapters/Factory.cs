﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Adapters
{
    public static class Factory
    {
        public enum AdapterType
        {
            Service,
            Controller,
        }
        public static AdapterType Adapter { get; set; } = Factory.AdapterType.Controller;

        public static CommonBase.Client.IDataAccess<TContract> Create<TContract>()
            where TContract : Contracts.IIdentifiable
        {
            string baseUri = "https://localhost:5001/api";
            CommonBase.Client.IDataAccess<TContract> result = null;

            if (Adapter == AdapterType.Controller)
            {
                if (typeof(TContract) == typeof(Contracts.Persistence.IGenre))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericControllerAdapter<Contracts.Persistence.IGenre, Transfer.Models.Persistence.Genre>();
                }
                else if (typeof(TContract) == typeof(Contracts.Persistence.IArtist))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericControllerAdapter<Contracts.Persistence.IArtist, Transfer.Models.Persistence.Artist>();
                }
                else if (typeof(TContract) == typeof(Contracts.Persistence.IAlbum))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericControllerAdapter<Contracts.Persistence.IAlbum, Transfer.Models.Persistence.Album>();
                }
                else if (typeof(TContract) == typeof(Contracts.Persistence.ITrack))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericControllerAdapter<Contracts.Persistence.ITrack, Transfer.Models.Persistence.Track>();
                }
                else
                    throw new ArgumentException($"The type {typeof(TContract).FullName} is not supported.");
            }
            else if (Adapter == AdapterType.Service)
            {
                if (typeof(TContract) == typeof(Contracts.Persistence.IGenre))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericServiceAdapter<Contracts.Persistence.IGenre, Transfer.Models.Persistence.Genre>(baseUri, "Genre");
                }
                else if (typeof(TContract) == typeof(Contracts.Persistence.IArtist))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericServiceAdapter<Contracts.Persistence.IArtist, Transfer.Models.Persistence.Artist>(baseUri, "Artist");
                }
                else if (typeof(TContract) == typeof(Contracts.Persistence.IAlbum))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericServiceAdapter<Contracts.Persistence.IAlbum, Transfer.Models.Persistence.Album>(baseUri, "Album");
                }
                else if (typeof(TContract) == typeof(Contracts.Persistence.ITrack))
                {
                    result = (CommonBase.Client.IDataAccess<TContract>)new GenericServiceAdapter<Contracts.Persistence.ITrack, Transfer.Models.Persistence.Track>(baseUri, "Track");
                }
                else
                    throw new ArgumentException($"The type {typeof(TContract).FullName} is not supported.");
            }
            return result;
        }
    }
}
