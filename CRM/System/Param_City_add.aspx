<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  >
    <title></title>
    <link href="../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />    
    <link href="../CSS/input.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>    
    
    <script src="../lib/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/common.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../JS/XHD.js" type="text/javascript"></script>
    <script src="../lib/jquery.form.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $.metadata.setType("attr", "validate");
            XHD.validate($(form1));

            //$("#T_Contract_name").focus();
            $("form").ligerForm();


            loadForm(getparastr("pid"), getparastr("stype"));


        });

        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=save&id=" + getparastr("pid") + "&stype=" + getparastr("stype");
                return $("form :input").fieldSerialize() + sendtxt;
            }
        }

        function loadForm(oaid,stype) {
            $.ajax({
                type: "GET",
                url: "../data/Param_City.ashx", /* ע���������ֶ�ӦCS�ķ������� */
                data: { Action: 'form', id: oaid, rnd: Math.random() }, /* ע������ĸ�ʽ������ */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    //alert(obj.constructor);
                    //String ���캯��
                    if (stype == "add") {
                        $("#T_Parent").ligerComboBox({
                            width: 180,
                            url: '../data/Param_City.ashx?Action=combo',
                            initValue: obj.id
                        })
                    }
                    else if (stype == "edit") {
                        $("#T_City").val(obj.City);
                        $("#T_order").val(obj.City_order);
                        $("#T_Parent").ligerComboBox({
                            width: 180,
                            url: '../data/Param_City.ashx?Action=combo',
                            initValue: obj.parentid
                        })
                    };
                    //if(obj.parentid==0)
                    //    $("#T_Parent").ligerGetComboBoxManager().setReadOnly();
                }
            });
        }
        
    </script>
</head>
<body style="margin:5px 5px 5px 5px">
    <form id="form1" onsubmit="return false">

        <table  align="left" border="0" cellpadding="3" cellspacing="1" 
            style="background: #fff; width:320px;">
            
            <tr>
                <td  width="65px"  ><div align="left" style="width: 61px">
                    �ϼ�������</div></td>
                <td   >
                    <div style="float:left; height: 20px;">
                        <input type="text" id="T_Parent" name="T_Parent"  validate="{required:true}" />
                    </div>                    
                </td>
            </tr>
            <tr>
                <td  >
                
                    <div align="left" style="width: 62px">��������</div></td>
                <td  >
                    <div style="float:left; height: 20px;">
                        <input type="text" id="T_City" name="T_City"   ltype='text' ligerui="{width:180}"  validate="{required:true}" />
                    </div>
                </td>
            </tr>
            <tr>
                <td  >
                
                    <div align="left" style="width: 62px">����</div></td>
                <td  >
                        <input type="text" id="T_order" name="T_order" value="10"  ltype='spinner' ligerui="{width:180}"  validate="{required:true}" /></td>
            </tr>
            <tr>
                <td  colspan="2" >
                
                    &nbsp;</td>
            </tr>
            </table>

    </form>
</body>
</html>