# Setup

Install Azure CLI, Terraform, Azurite (https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite).

Setup Terraform:
https://docs.microsoft.com/en-us/azure/developer/terraform/quickstart-configure

Then:

```bash
cd /infra
terraform init
```

# Deploy

```bash
terraform plan
terraform apply
```

# Run

```bash
azurite -s -l c:\dev\azurite -d c:\dev\azurite\debug.log
```

Then start Consumer and Producer projects in parallel.

# Notes

## Azure Queue Storage

Basic queue service.

Use case: cost effective to handle high volume of messages

[Docs](https://docs.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction)
[Comparison to ServiceBus](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-azure-and-service-bus-queues-compared-contrasted)