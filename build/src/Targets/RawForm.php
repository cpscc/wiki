<?php
namespace Cornerstone\Quarry\Targets;

class RawForm
{
    function convert(array $spec, array $config)
    {
        $config = $config[$spec['version']];
        echo "$spec[method] $config[path]$spec[path] HTTP/1.1\n";
        echo "Host: $config[endpoint]\n";
        echo "Accept: application/x-www-form-urlencoded\n";
    }
}
