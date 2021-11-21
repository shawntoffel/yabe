# The YABE project
Yet Another Blob Explorer. Browse, upload, and delete Azure Blob Storage blobs.


This project is a Blazor web app using the server hosting model. User login provided by AzureAD.

Configuration Example:
```json
  "ConnectionStrings": {
      "AzureBlobStorage": "your blob storage connection string"
  },
  "Yabe": {
      "Title": "Yabe",
      "SubTitle": "Yet Another Blob Explorer",
      "EditorRole": "Editor",
      "MaxUploadBytes": 15728640,
      "MaxFilesPerUpload": 10
  },
  "AzureAd": {
      "Instance": "https://login.microsoftonline.com/",
      "TenantId": "your ad app tenant id",
      "ClientId": "your ad app client id",
      "CallbackPath": "/signin-oidc",
      "SignedOutCallbackPath": "/signout-oidc"
  }
```




