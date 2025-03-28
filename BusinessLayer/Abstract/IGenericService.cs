﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        void TAdd( T t);
        void TInsert(T t);
        void TUpdate(T T);
        void TDelete(T t);
        List<T> GetList();
        T GetById(int id);

    }
}
