function removeAttachment() {
    document.getElementById("lblAttachment").innerHTML = "";
    document.getElementById("valAttachment").value = "";
    document.getElementById("containerAttachmentFile").innerHTML = "";
    document.getElementById("containerAttachmentFile").innerHTML = "<input type='file' id='AttachmentFile' name='AttachmentFile' />";
}