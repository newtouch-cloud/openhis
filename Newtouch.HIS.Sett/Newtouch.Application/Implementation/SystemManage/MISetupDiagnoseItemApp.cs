/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    public class MISetupDiagnoseItemApp : IMISetupDiagnoseItemApp
    {

        private readonly IMIDiagnosisAndTreatmentItemRepo _yb_setup_zlxmRepository;

        public MISetupDiagnoseItemApp(IMIDiagnosisAndTreatmentItemRepo yb_setup_zlxmRepository)
        {
            this._yb_setup_zlxmRepository = yb_setup_zlxmRepository;
        }

        public List<MIDiagnosisAndTreatmentItemEntity> GetList()
        {
            return _yb_setup_zlxmRepository.IQueryable().ToList();
        }
        public MIDiagnosisAndTreatmentItemEntity GetForm(string keyValue)
        {
            return _yb_setup_zlxmRepository.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            _yb_setup_zlxmRepository.Delete(t => t.setup_zlxmbh == int.Parse(keyValue));
        }
        public void SubmitForm(MIDiagnosisAndTreatmentItemEntity zlxmEntity, string keyValue)
        {
            //if (!string.IsNullOrEmpty(keyValue))
            //{
            //    zlxmEntity.setup_zlxmbh = keyValue;
            //    zlxmEntity.Modify(keyValue);
            //    _yb_setup_zlxmRepository.Update(zlxmEntity);
            //}
            //else
            //{
            //    zlxmEntity.setup_zlxmbh = Comm.GuId();
            //    zlxmEntity.Create();
            //    _yb_setup_zlxmRepository.Insert(zlxmEntity);
            //}
        }
    }
}
