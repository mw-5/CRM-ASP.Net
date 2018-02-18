function goToStart()
{
    location.href = '/Start/Start';
}

function goToListOfCustomers()
{
    location.href = '/ListOfCustomers/ListOfCustomers';
}

function search(msg)
{
    var cid = document.getElementById("txtSearch").value;
    navTo(cid, "/Cockpit/Search?cid=", msg);
}

function newContactPerson(msg)
{
    var cid = document.getElementById("lblCid").innerHTML;
    navTo(cid, "/FrmContactPerson/New?cid=", msg);
}

function editContactPerson(msg)
{
    var id = document.getElementById("idSelectedContactPerson").value
    navTo(id, "/FrmContactPerson/Edit?id=", msg);
}

function newNote(msg)
{
    var cid = document.getElementById("lblCid").innerHTML;
    navTo(cid, "/FrmNote/New?cid=", msg);
}

function editNote(msg)
{
    var id = document.getElementById("idSelectedNote").value
    navTo(id, "/FrmNote/Edit?id=", msg);
}

function openAttachment(msg)
{
    var id = document.getElementById("idSelectedNote").value
    navTo(id, "/FrmNote/OpenAttachment?id=", msg);
}

function navTo(id, url, msg)
{
    if (id != undefined && !isNaN(id) && id != 0)
    {
        location.href = url + id;
    }
    else
    {
        alert(msg);
    }
}

