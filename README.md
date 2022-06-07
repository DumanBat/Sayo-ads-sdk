# Sayo-ads-sdk
The SDK contains two main prefabs with attached scripts: AdPlayer and Purchase.

# AdPlayer
AdPlayer is a prefab with a world space canvas and video player attached to it. The main purpose of AdPlayer is to play video in the world space downloaded using VAST XML.
For its operation, you must provide a link to VAST XML and set the video file format. The rest of the options, like the file name or the render texture, are needed solely for customization purposes. AdPlayer sends GET request for link provided in VAST URL.

![image](https://user-images.githubusercontent.com/76429190/172427302-e99c2ea7-b612-4aa6-b9fc-cb828b87be31.png)
![image](https://user-images.githubusercontent.com/76429190/172425823-b872845a-66e4-4cde-a27c-a37c57f72ff2.png)


# Purchase
The PurchaseView displays in its window information about the item received in the JSON by the link ItemURL as a response of the POST request. When pressing the Submit button, the data is checked and sent to the address specified in PurchaseURL as POST request.

![image](https://user-images.githubusercontent.com/76429190/172428487-df66c9e0-5d19-48cd-8e10-9870b974a8cc.png)
![image](https://user-images.githubusercontent.com/76429190/172425920-5774e428-b07c-47c1-bcf0-fea160a6d20d.png)
