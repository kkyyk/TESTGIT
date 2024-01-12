/**
* @name: Parsley 自定義Validate Function
* @version: 1.0.0
* @copyright: Copyright © 2022.
*/

jQuery(function ($) {
    ///* 忽略驗證類型(全域)，由於官方文件未提供此設定方法，改為依照文件方法直接設定在 form 上，請全域搜尋 @data_parsley_excluded */
    //window.Parsley.options.excluded = "input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden, .d-none";

    //檔案大小限制(MB)
    window.Parsley.addValidator('maxFileSize', {
        validateString: function (_value, maxSize, parsleyInstance) {

            if (!window.FormData) {
                alert('You are making all developpers in the world cringe. Upgrade your browser!');
                return true;
            }
            // 錯誤訊息
            window.Parsley.addMessage('zh-tw', 'maxFileSize', '檔案大小不可超過 %s Mb.');
            // 判斷檔案大小
            var totalFileSize = 0;
            var files = parsleyInstance.$element[0].files;
            $(files).each(function (idx, obj) {
                totalFileSize = totalFileSize + obj.size;
            });
            return totalFileSize <= maxSize * 1024 * 1024;
        },
        requirementType: 'integer'
    });

    //單一檔案大小限制(MB)
    window.Parsley.addValidator('maxSingleFileSize', {
        validateString: function (_value, maxSize, parsleyInstance) {

            if (!window.FormData) {
                alert('You are making all developpers in the world cringe. Upgrade your browser!');
                return true;
            }
            // 錯誤訊息
            window.Parsley.addMessage('zh-tw', 'maxSingleFileSize', '單一檔案大小不可超過 %s Mb.');

            // 判斷單檔大小
            var isSuccess = true;
            var files = parsleyInstance.$element[0].files;            
            $(files).each(function (idx, obj) {
                if (obj.size > maxSize * 1024 * 1024) {
                    isSuccess = false;
                }
            });
            return isSuccess;
        },
        requirementType: 'integer'
    });

    //檔案數量限制
    window.Parsley.addValidator('maxFileCount', {
        validateString: function (_value, maxCount, parsleyInstance) {
            if (!window.FormData) {
                alert('You are making all developpers in the world cringe. Upgrade your browser!');
                return true;
            }
            // 錯誤訊息
            window.Parsley.addMessage('zh-tw', 'maxFileCount', '上傳的檔案數量不可超過 %s 個.');

            // 判斷檔案數量
            var files = parsleyInstance.$element[0].files;
            return files.length <= maxCount;
        },
        requirementType: 'integer'
    });

    //檔案類型限制(以逗號分隔副檔名，EX:PDF,DOC,DOCX <大小寫沒差>)
    window.Parsley.addValidator('fileType', {
        validateString: function (_value, fileType, parsleyInstance) {
            if (!window.FormData) {
                alert('You are making all developpers in the world cringe. Upgrade your browser!');
                return true;
            }
            // 錯誤訊息
            window.Parsley.addMessage('zh-tw', 'fileType', '上傳的檔案類型，只能是 %s');

            // 判斷檔案數量
            var fileTypes = fileType.toLowerCase().split(',');
            var ok = true;
            var files = parsleyInstance.$element[0].files;
            $(files).each(function (idx, obj) {
                var fileNameSplit = obj.name.split(".");
                var fileExtension = fileNameSplit[fileNameSplit.length - 1];
                if (jQuery.inArray(fileExtension.toLowerCase(), fileTypes) < 0) {
                    ok = false;
                    return false; //等同於break
                }
            });
            return ok;
        },
        requirementType: 'string'
    });

    /** 
     * code sample:
     *  data_parsley_phone = "" - 市話/手機
     *  data_parsley_phone = "tel" - 市話
     *  data_parsley_phone = "mobile" - 手機
     */
    window.Parsley.addValidator('phone', {
        validateString: function (value, type) {
            let regex;
            switch (type) {
                case "tel":
                    regex = new RegExp("^0[2-8]\\d{0,1}-?(\\d{6,8})(#\\d{1,5}){0,1}$");
                    break;
                case "mobile":
                    regex = new RegExp("^09\\d{2}(\\d{6}|\\d{3}\\d{3})$");
                    break;
                default:
                    regex = new RegExp("^0[2-8]\\d{0,1}-?(\\d{6,8})(#\\d{1,5}){0,1}$|^09\\d{2}(\\d{6}|\\d{3}\\d{3})$");
                    break;
            }

            return regex.test(value);
        },
        requirementType: 'string',
        messages: {
            'en': 'Please enter a valid phone format.',
            'zh-tw': '請填入有效電話號碼'
        }
    });

    /** 
     * code sample:
     *  data_parsley_psw = "" - 密碼至少8碼以上(含)，及英文大寫或小寫、數字、特殊符號
     */
    window.Parsley.addValidator('psw', {
        validateString: function (value) {
            let regex = new RegExp("^(?=.*\\d)(?=.*[a-zA-Z])(?=.*\\W).{8,}$");

            return regex.test(value);
        },
        requirementType: 'string',
        messages: {
            'en': 'The password must be at least 8 digits (inclusive), and must include English uppercase letters, English lowercase letters, numbers, and special symbols. Choose three of the four that match..',
            'zh-tw': '密碼至少8碼以上(含)，及英文大寫或小寫、數字、特殊符號'
        }
    });

    /** 
 * code sample:
 *  data_parsley_frontendat7 = "" - 長度6至12字元長度，可填寫英文大小寫或數字，字間不可輸入空白
 */
    window.Parsley.addValidator('frontendat7', {
        validateString: function (value) {
            let regex = new RegExp("^[a-zA-Z0-9]{6,12}$");

            return regex.test(value);
        },
        requirementType: 'string',
        messages: {
            'en': 'Password should over 8 characters, at least one alphabetic character, one number and one special character.',
            'zh-tw': '帳號需符合長度6至12碼，可填寫英文大小寫或數字，字間不可輸入空白。'
        }
    });

    /** 
 * code sample:
 *  data_parsley_frontendpsw = "" - 8碼以上，其組成必須至少有一個特殊符號、一個大寫或小寫字母、一個數字
 */
    window.Parsley.addValidator('frontendpsw', {
        validateString: function (value) {
            let regex = new RegExp("^(?=.*\\d|.*\\W)(?=.*[a-zA-Z]).{8,}$");

            return regex.test(value);
        },
        requirementType: 'string',
        messages: {
            'en': 'Password should over 8 characters, at least one alphabetic character, one number and one special character.',
            'zh-tw': '密碼須符合長度8碼以上，並混合大小寫英文字母、數字或特殊符號。'
        }
    });

    /** 
     * code sample:
     *  data_parsley_idnumber = "" - 身分證或居留證號
     *  deps: taiwan-id-validator2.min.js
     */
    window.Parsley.addValidator('idnumber', {
        validateString: function (value) {
            return taiwanIdValidator.isNationalIdentificationNumberValid(value) || taiwanIdValidator.isResidentCertificateNumberValid(value);
        },
        requirementType: 'string',
        messages: {
            'en': 'Please enter a valid id number or resident certificate number.',
            'zh-tw': '請輸入合法身分證字號或居留證編號'
        }
    });

    window.Parsley.addValidator('notidnumber', {
        validateString: function (value) {
            return !(taiwanIdValidator.isNationalIdentificationNumberValid(value) || taiwanIdValidator.isResidentCertificateNumberValid(value));
        },
        requirementType: 'string',
        messages: {
            'en': 'Can\'t be an id number or resident certificate number.',
            'zh-tw': '不可為身分證字號或居留證編號'
        }
    });

    /** 
     * code sample:
     *  data_parsley_sid = "" - NTU-學生證號
     */
    window.Parsley.addValidator('sid', {
        validateString: function (value) {
            let regex = new RegExp("^[A-Z]{1}[0-9]{8,9}$");

            return regex.test(value);
        },
        requirementType: 'string',
        messages: {
            'en': 'Please enter a valid student id number.',
            'zh-tw': '請輸入正確學生證號格式'
        }
    });

    ///** 
    // * code sample:
    // *  data_parsley_test = "" - 
    // */
    //window.Parsley.addValidator('test', {
    //    validateString: function (value, requeriment, instance, element ) {
    //        console.log(value); console.log(requeriment);
    //        console.log(instance); console.log(element);

    //        return true;
    //    },
    //    requirementType: 'boolean',
    //    messages: {
    //        'en': '',
    //        'zh-tw': ''
    //    }
    //});

    /** 
     * code sample:
     *  data_parsley_regex_example = ""
     * 
     */
    window.Parsley.addValidator('regexExample', {
        validateString: function (value) {
            var regx = new RegExp("^([a-z0-9]{5,})$");

            return regx.test(value);
        },
        requirementType: 'string',
        messages: {
            en: 'regex_ex wrong format',
            'zh-tw': 'regex_ex 格式錯誤'
        }
    });


    window.Parsley.addValidator('checkAua8Twice', {
        validateString: function (value, checkObjId) {
            return $("#" + checkObjId).val() == value;
        },
        requirementType: 'string',
        messages: {
            en: 'not same',
            'zh-tw': '兩次輸入的密碼不相同'
        }
    });

    /**
   * code sample:
   * data_parsley_frontend_Change_Check_Aua8_Twice = "checkObjId"
   * data-parsley-frontend-Change-Check-Aua8-Twice = "checkObjId"
   * 前台變更密碼再次確認二次密碼 
   */
    window.Parsley.addValidator('frontendChangeCheckAua8Twice', {
        validateString: function (value, checkObjId) {
            return $("#" + checkObjId).val() == value;
        },
        requirementType: 'string',
        messages: {
            en: 'not same',
            'zh-tw': '再次輸入密碼不相同'
        }
    });

    window.Parsley.addValidator('yearsCompares', {
        validateString: function (value, checkObjId) {
            var sDate = Number($("#" + checkObjId).val());
            var eDate = Number(value);
            
            return eDate >= sDate;
        },
        requirementType: 'string',
        messages: {
            en: 'not same',
            'zh-tw': '結束年份不得小於開始年份'
        }
    });

    /**
     * code sample:
     *  data_parsley_positive_integer = "" - 正整數
     */
    window.Parsley.addValidator('positiveInteger', {
        validateString: function (value) {
            //var regx = new RegExp("^(\\d*)(\.0*)?$");
            var regx = new RegExp("^\\d*$");

            return regx.test(value);
        },
        requirementType: 'integer',
        messages: {
            en: 'Please enter positive integer.',
            'zh-tw': '請輸入正整數'
        }
    });


});



