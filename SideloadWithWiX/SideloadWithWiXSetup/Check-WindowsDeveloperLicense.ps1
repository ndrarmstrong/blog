# Check for a Windows developer license
# If one does not exist, prompt the user
# to install one on the machine

$hasDevLicense = $FALSE;

# Check for license function
Function CheckForLicense
{
    Try
    {
        $licenseInfo = Get-WindowsDeveloperLicense
        if ($licenseInfo.IsValid)
        {
            Write-Host "Found a valid developer license"
            return $TRUE
        }
    }
    Catch
    {
        Write-Host "No (valid) developer license is present"
    }

    return $FALSE
}

# Get a license if we don't have one
$hasDevLicense = CheckForLicense

if (!$hasDevLicense)
{
    Try
    {
        Show-WindowsDeveloperLicenseRegistration
    }
    Catch
    {
        Write-Host "License registration was cancelled by user, or failed"
    }

    $hasDevLicense = CheckForLicense
}

if (!$hasDevLicense)
{
  exit 1
}