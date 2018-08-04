<?php
namespace Cornerstone\Quarry\Targets;

class RawForm
{
    function convert(array $json)
    {
        echo "$json[method] $json[path] HTTP/1.1\n";
        echo "Host: api.cornerstone.cc\n";
        echo "Accept: application/x-www-form-urlencoded\n";
    }
}
