<?php
namespace Cornerstone\Quarry\Targets;

class RawForm
{
    const name = "raw.x-www-form-urlencoded";

    function convert(array $spec, array $config)
    {
        $config = $config[$spec['version']];
        $out = "";
        $out.= "$spec[method] $config[path]$spec[path] HTTP/1.1\n";
        $out.= "Host: $config[endpoint]\n";
        $out.= "Accept: application/x-www-form-urlencoded\n";
        return $out;
    }
}
// TODO: use a $handle instead of a string
