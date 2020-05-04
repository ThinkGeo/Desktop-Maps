# TransactionResultStatus

**Summary**

N/A

**Remarks**

N/A

**Items**

|Name|Description|
|---|---|
|Success|This means that each item in the transaction buffer succeeded.|
|Failure|This means that at least one of the items in the transaction buffer failed. It may mean that other record did succeed. This depends on the implementation of the specific FeatureSource.|
|Cancel|This means that the transaction was canceled.|

