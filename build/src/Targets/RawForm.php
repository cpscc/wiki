<?php
namespace Cornerstone\Quarry\Targets;

class RawForm extends Raw
{
    const name = "raw.x-www-form-urlencoded";
    const ctype = "application/x-www-form-urlencoded";

    function convert(array $spec, array $config, $h)
    {
        parent::convert($spec, $config, $h);
    }
}
