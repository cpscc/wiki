<?php
namespace Cornerstone\Quarry\Targets;

class RawForm
{
    function convert(array $json, array $config)
    {
        echo "$json[method] $config[path]$json[path] HTTP/1.1\n";
        echo "Host: $config[endpoint]\n";
        echo "Accept: application/x-www-form-urlencoded\n";
    }
}
