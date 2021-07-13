<?php
namespace Cornerstone\Quarry\Targets;

class RawJson extends Raw
{
    const name = "raw.json";
    const ctype = "application/json";

    function convert(array $spec, array $config, $h)
    {
        parent::convert($spec, $config, $h);
    }
}

