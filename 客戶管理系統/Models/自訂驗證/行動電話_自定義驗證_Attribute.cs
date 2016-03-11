using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace 客戶管理系統.Models.自訂驗證 {

    public class 行動電話_自定義驗證_Attribute : DataTypeAttribute {

        public 行動電話_自定義驗證_Attribute()
            : base(DataType.Text) {
        }

        //* 實作一個「自訂輸入驗證屬性」可驗證「手機」的電話
        //格式必須為 "\d{4}-\d{6}" 的格式 ( e.g. 0911-111111 )
        public override bool IsValid(object value) {
            Regex rgx = new Regex(@"\d{4}-\d{6}");
            return rgx.IsMatch((value == null) ? "" : value.ToString());
        }

    }

}