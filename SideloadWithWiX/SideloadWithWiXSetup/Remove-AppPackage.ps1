# Removes an app package, if it exists

param(
    [string]$packageName
)

if (!$packageName)
{
    echo "No package name argument provided"
    exit 1
}

echo "Finding package info for package '$packageName'"
$package = (Get-AppxPackage | Where-Object {$_.Name -eq "$packageName"})

if (!$package)
{
    echo "Could not locate package info for $packageName (user may have uninstalled)"
    exit 0
}

$packageFullName = $package.PackageFullName
echo "Removing $packageFullName"
return Remove-AppxPackage $packageFullName